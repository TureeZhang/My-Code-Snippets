<https://angular.cn/guide/deployment#routed-apps-must-fallback-to-indexhtml>

## 带路由的应用必须以 index.html 作为后备页面

Angular 应用很适合用简单的静态 HTML 服务器提供服务。 你不需要服务端引擎来动态合成应用页面，因为 Angular 会在客户端完成这件事。

如果该应用使用 Angular 路由器，你就必须配置服务器，让它对不存在的文件返回应用的宿主页(index.html)。

带路由的应用应该支持“深链接”。 所谓深链接就是指一个 URL，它用于指定到应用内某个组件的路径。 比如，http://www.mysite.com/heroes/42 就是一个到英雄详情页面的深链接，用于显示 id: 42 的英雄。

当用户从运行中的客户端应用导航到这个 URL 时，这没问题。 Angular 路由器会拦截这个 URL，并且把它路由到正确的页面。

但是，当从邮件中点击链接或在浏览器地址栏中输入它或仅仅在英雄详情页刷新下浏览器时，所有这些操作都是由浏览器本身处理的，在应用的控制范围之外。 浏览器会直接向服务器请求那个 URL，路由器没机会插手。

静态服务器会在收到对 http://www.mysite.com/ 的请求时返回 index.html，但是会拒绝对 http://www.mysite.com/heroes/42 的请求， 并返回一个 404 - Not Found 错误，除非，它被配置成了返回 index.html。

## 后备页面配置范例

没有一种配置可以适用于所有服务器。 后面这些部分会描述对常见服务器的配置方式。 这个列表虽然不够详尽，但可以为你提供一个良好的起点。

- **NGinx**：

使用 try_files 指向 index.html，详细描述见Web 应用的前端控制器模式。

```code
try_files $uri $uri/ /index.html;
```
