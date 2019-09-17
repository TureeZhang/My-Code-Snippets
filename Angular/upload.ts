/* https://github.com/NG-ZORRO/ng-zorro-antd/blob/master/components/upload/nz-upload-btn.component.html */

`
<input type="file" #file (change)="onChange($event)"
  [attr.accept]="options.accept"
  [attr.directory]="options.directory ? 'directory': null"
  [attr.webkitdirectory]="options.directory ? 'webkitdirectory': null"
  [multiple]="options.multiple" style="display: none;">
<ng-content></ng-content>
`;

@ViewChild('file', { static: false }) file: ElementRef;

@HostListener('click')
onClick(): void {
  if (this.options.disabled || !this.options.openFileDialogOnClick) {
    return;
  }
  (this.file.nativeElement as HTMLInputElement).click();
}
