  //Observable 对象 https://blog.csdn.net/qq_35592166/article/details/78281030
  buildQiniuUploadParameters(file: UploadFile, fileList: UploadFile[]): Observable<boolean> {
    return Observable.create(observer => {

      let uploadFullName = this.host.directoryPath + "/" + file.name;
      let pars = new QiniuUploadParameters();

      this.host.qiniuUploadService.getQiniuUploadToken(file.filename).subscribe(token => {
        pars.key = file.name;
        pars.token = token;
        this.host.qiniuUploadParameters = pars;
      }, () => {
        observer.next(false);
        observer.complete();
      }, () => {
        observer.next(true);
        observer.complete();
      });

    });

    return result;
  }


//视图上可以使用 |async 管道处理可观察对象

<button *ngIf="pageStatus==0 && (isAdmin|async)" nz-button nzType="primary" (click)="edit()" style="margin-left:16px;"><i nz-icon type="edit"></i>编 辑</button>
