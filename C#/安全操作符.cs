// ?. 相当于 if(...!=null){ } ，避免 Null 引用异常
new Service(this.SessionId).GetServiceStatus(appId)?.Status
