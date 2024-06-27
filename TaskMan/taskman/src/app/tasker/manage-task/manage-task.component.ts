import { Component, Input, OnChanges, OnInit, SimpleChanges, inject } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { TaskServiceService } from '../../task-service.service';
import { taskModel } from '../taskModel';
import { NgbActiveOffcanvas, NgbToast, NgbToastModule } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-manage-task',
  templateUrl: './manage-task.component.html',
  styleUrl: './manage-task.component.scss',
})
export class ManageTaskComponent implements OnInit, OnChanges {

  taskForm!: FormGroup;
  taskStatus = [];
  @Input() taskData!: taskModel;
  activeOffcanvas = inject(NgbActiveOffcanvas);
  success = false;
  constructor(private fb: FormBuilder, private taskService: TaskServiceService) { }
  ngOnChanges(changes: SimpleChanges): void {
    if (changes["taskData"]) {
      this.taskForm.patchValue(this.taskData);
    }
  }
  ngOnInit(): void {
    this.taskForm = this.fb.group({
      id: new FormControl(''),
      title: new FormControl('', [Validators.required]),
      description: new FormControl(''),
      status: new FormControl('')
    });

    this.getStatus();
    if (this.taskData) {
      this.taskForm.patchValue(this.taskData);
    }
  }

  get taskFormGroup() {
    return this.taskForm.controls;
  }

  close() {
    this.activeOffcanvas.close();
  }

  getStatus() {
    this.taskService.getStatus().subscribe((res: any) => {
      console.log(res);
      this.taskStatus = res;
    });
  }

  saveTask() {
    if (this.taskForm.valid) {
      this.taskService.postTask(this.taskForm.value).subscribe((res: any) => {
        console.log(res);

        this.success = true;
      })
    }
  }
}
