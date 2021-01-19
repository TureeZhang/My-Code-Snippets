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
python setup.py sdist bdist_wheel
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

## 如果仅分发 pyc 而不是源码 pv

创建 MANIFEST.in 文件，包含如下内容 <https://stackoverflow.com/questions/38394362/distribute-pip-package-no-source-code>

官方文档 https://packaging.python.org/guides/using-manifest-in/

```
global-include *.py[co]
global-exclude *.py
```

此模板文件决定创建包时，如何包含或排除内容。包含 pvc 而排除 pv 即可。

有时，此文件会受编码格式影响，例如使用 UTF8 无法识别而使用 GB2312 可以，需要留意 python setup.py sdist bdist_wheel 命令输出的内容中是否包含警告，以及观察输出中的 copy 文件信息是否包含目标文件。否则打包之后的包，在安装时无法正确输出文件，错误的 python 包仅为 2KB 大小，通常一定是比这个体积大一些的。

## Nexus 配置匿名访问

默认拉取需要用户名和密码，但 Nexus 支持设置匿名账户。

注意 annoymouse 匿名用户需要配置角色，角色需要给 nexus-pypi-\*-read 等权限，按照实际情况设置。
