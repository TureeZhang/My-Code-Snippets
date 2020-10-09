两种办法：

A.

使用 |Async 管道。
```html
<div ngIf="(settings|async).url"></div>
```

B.

```html
<div *ngIf="isLoading else loading"></div>
<ng-template loading></ng-template>
```
