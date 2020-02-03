<https://github.com/Microsoft/TypeScript/issues/25642#issuecomment-452255528>

How you guys solve this?

```ts
use String(process.env) == 'development'
```

Typecasting :)

最后采用 ``<ng-container *ngIf="usage.toString()=='1' ....``  通过了 npm run build -- --prod 编译 
