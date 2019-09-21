        /// <summary>
        /// 用户登录校验。
        /// </summary>
        /// <param name="userInfoDto"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult Post([FromBody]UserInfoDto userInfo)  //记得使用 [FromBody] 标记值来自于请求体
        {
            UserInfoDto result = this._userInfoService.Login(userInfo);
            return new JsonResult(result);
        }
