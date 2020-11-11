pv:

```yaml
apiVersion: v1
kind: PersistentVolume
metadata:
  name: turee-project
spec:
  capacity:
    storage: 2Gi
  accessModes:
    - ReadWriteOnce
  persistentVolumeReclaimPolicy: Retain
  nfs:
    server: 10.0.1.24 # NFS 服务器地址
    path: "/k8s/my-server" # 如不存在，必须手动创建此路径
```

pvc:

```yaml
apiVersion: v1
kind: PersistentVolumeClaim
metadata:
  name: my-server-pvc
  namespace: turee-project
spec:
  accessModes:
    - ReadWriteOnce
  resources:
    requests:
      storage: 2Gi
  volumeName: turee-project
```

deployment:

```yaml
      volumes:
        - name: my-server-pvc
          persistentVolumeClaim:
            claimName: my-server-pvc
    spec:
      containers:
        - image: 'image:1.0.0'
          volumeMounts:
            - mountPath: /myserver/projects/bla
              name: my-server-pvc
```
