使用代码提交表单，并在校验不通过时禁用提交。

```html
<ng-template #createProjectModalTitle>
  <span>新建项目</span>
</ng-template>
<ng-template #createProjectModalContent>
  <div>
    <form nz-form nzLayout="vertical" [formGroup]="createProjectForm" (ngSubmit)="onSubmit()" #apiProjectFormElementRef="ngForm">
      ...
</ng-template>
<ng-template #createProjectModalFooter let-ref="modalRef">
  <button nz-button (click)="ref.destroy()">取消</button>
  <button nz-button nzType="primary" type="submit" (click)="createProject()" [disabled]="!apiProjectForm.valid">创建</button>
</ng-template>
```

```ts
  @ViewChild("apiProjectFormElementRef")
  apiProjectFormElementRef!: NgForm;

  createProject(): void {
    this.apiProjectFormElementRef.ngSubmit.emit();
  }
  
  onSubmit(): void {

      for (const i in this.apiProjectForm.controls) {
        this.apiProjectForm.controls[i].markAsDirty();
        this.apiProjectForm.controls[i].updateValueAndValidity();
      }

      this.isFormSubmiting = true;
      ....
  }
```
