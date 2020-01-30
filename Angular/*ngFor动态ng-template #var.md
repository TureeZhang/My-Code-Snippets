<https://stackoverflow.com/questions/44440879/dynamic-template-reference-variable-inside-ngfor-angular-2>

#elementRef 获得引用的模板，# 作为引用的一个语法，是有作用域的。当你外围有 *ngFor 时，则 #elementRef 不需动态的换新名字，直接赋值给 [nzAvatar] 属性即可。

```ts
    <div nz-col nzSpan="6" *ngFor="let item of data">
      <nz-card style="width: 100%;margin-top: 16px" nzHoverable="true"
               routerLink="data.routerLink">
        <nz-card-meta [nzAvatar]="elementRef"
                      [nzTitle]="item.title"
                      [nzDescription]="item.description"></nz-card-meta>
      </nz-card>
      <ng-template #elementRef>
        <i nz-icon nzType="item.iconType" nzTheme="outline"></i>
      </ng-template>
    </div>
```

