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

