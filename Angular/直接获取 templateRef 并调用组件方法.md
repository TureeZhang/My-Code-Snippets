```html
<ng-template #createProjectModalFooter let-ref="modalRef">
  <button nz-button (click)="ref.destroy()">取消</button>
  <button nz-button nzType="primary" (click)="submitForm()" [nzLoading]="isFormSubmiting">创建</button>
</ng-template>
```

这将直接在模板内关闭 Modal 弹框。
