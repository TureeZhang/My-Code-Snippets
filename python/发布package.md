## 工具

```shell
# 打包工具
python -m pip install --user --upgrade setuptools wheel
# 上传工具
python -m pip install --user --upgrade twine
```

## 文件结构

```
F:.
├───LICENSE
├───setup.py
├───README.md
├───myapi
│   ├───filters
│   ├───models
│   └───services
```

## 进行打包

输出文件在 dist

```shell
python -m pip install --user --upgrade setuptools wheel
```

最终文件结构

```
├───LICENSE
├───setup.py
├───README.md
├───myapi
│   ├───filters
│   ├───models
│   └───services
├───myapi.egg-info
├───build
│   └───bdist.win-amd64
├───dist
│   ├───myapi-0.0.1.tar.gz
│   └───myapi-0.0.1-py3-none-any.whl
```

## 上传

```shell
python -m twine upload --repository-url https://repo.xxxx.com/repository/pypi-host/ dist/*
```

## 下载安装

注意，是 https 不是 http，并且 url 结尾 simple 不能少

```shell
python -m pip install --extra-index-url https://repo.xxx.com/repository/pypi-group/simple myapi==0.0.1
```

## Nexus 配置匿名访问

annoymouse 匿名用户需要配置角色，角色需要给 nexus-pypi-\*-read 等权限，按照实际情况设置。
