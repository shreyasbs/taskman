import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { taskModel } from './tasker/taskModel';

@Injectable({
  providedIn: 'root'
})
export class TaskServiceService {
  api = environment.api;
  constructor(private http: HttpClient) { }

  getTasks() {
    return this.http.get(this.api + 'tasks/GetTasks');
  }

  getStatus() {
    return this.http.get(this.api + 'tasks/GetTaskStatus');
  }

  postTask(taskObj: taskModel) {
    return this.http.post(this.api + 'tasks/PostTask', taskObj);
  }

  deleteTask(id: string) {
    const httpParams = new HttpParams().set("id", id);

    return this.http.delete(this.api + 'tasks/DeleteTask', { params: httpParams });
  }
}
