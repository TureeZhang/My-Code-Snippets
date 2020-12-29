以下方式使用 VS 调试启动 uvicorn 并允许 --reload 参数传入:

```py
#main.py
import uvicorn  #debug in VS (Code) → https://fastapi.tiangolo.com/tutorial/debugging/
from fastapi import FastAPI

app = FastAPI()

@app.get( "/" )
async def root():
    return {"message":"Hello World"}

@app.get( "/2" )
async def root():
    return {"message":"Hello World"}

if(__name__ == "__main__"):
    uvicorn.run( "main:app",host="0.0.0.0",port=8000,reload=True )
```

如果你的文件名是 my-uvicorn-api ，对象名是 new_app，则传入参数是 "my-uvicorn-api:new_app"。
