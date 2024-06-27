import { Component, OnInit, QueryList, ViewChildren, inject } from '@angular/core';
import { TaskServiceService } from '../task-service.service';
import { taskModel } from './taskModel';
import { NgbOffcanvas } from '@ng-bootstrap/ng-bootstrap';
import { ManageTaskComponent } from './manage-task/manage-task.component';

@Component({
  selector: 'app-tasker',
  templateUrl: './tasker.component.html',
  styleUrl: './tasker.component.scss'
})
export class TaskerComponent implements OnInit {
  tasks!: taskModel[];
  offcanvasService = inject(NgbOffcanvas);
  taskStatus = [];
  orgTasks!: taskModel[];
  sortKey: string = 'id';
  sortOrder: 'asc' | 'desc' = 'asc';

  constructor(private taskService: TaskServiceService,) { }
  ngOnInit(): void {
    this.getTasks();
    this.getStatus();
  }

  getStatus() {
    this.taskService.getStatus().subscribe((res: any) => {
      console.log(res);
      this.taskStatus = res;
    });
  }

  getTasks() {
    this.taskService.getTasks().subscribe((res: any) => {
      this.tasks = res;
      this.orgTasks = JSON.parse(JSON.stringify(res));
    })
  }

  manageTask(taskObj?: taskModel) {
    const offcanvasRef = this.offcanvasService.open(ManageTaskComponent, { backdrop: 'static' });
    offcanvasRef.componentInstance.taskData = taskObj;
    offcanvasRef.result.then((res: any) => {
      this.getTasks();
    })
  }

  deleteTask(id: any) {
    this.taskService.deleteTask(id).subscribe((res: any) => {
      this.getTasks();
    });
  }

  statusChangeFilter(eventTarget: any) {
    if (eventTarget.value == 'All') {
      this.tasks = this.orgTasks;
    } else {
      this.tasks = this.orgTasks.filter(x => x.status == eventTarget.value);
    }

  }

  sortTasks(key: string) {
    if (this.sortKey === key) {
      this.sortOrder = this.sortOrder === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortKey = key;
      this.sortOrder = 'asc';
    }

    this.tasks.sort((a: any, b: any) => {
      if (a[key] < b[key]) {
        return this.sortOrder === 'asc' ? -1 : 1;
      } else if (a[key] > b[key]) {
        return this.sortOrder === 'asc' ? 1 : -1;
      } else {
        return 0;
      }
    });
  }


}
