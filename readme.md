# AWS Lightsail + Github Actions

## Setup AWS Lightsail

```shell
aws lightsail create-container-service \
    --service-name demo-service \
    --power nano \
    --scale 1

aws lightsail create-relational-database\
    --relational-database-name demo-db \
    --relational-database-blueprint-id postgres_12 \
    --relational-database-bundle-id micro_2_0 \
    --master-database-name db \
    --master-username db_user \
    --master-user-password db_password
```

## Build Docker Images

```shell
docker build -t web -f Web/Dockerfile .
docker build -t service -f Service/Dockerfile .
```

## Push Docker Iamges

```shell
aws lightsail push-container-image --service-name demo-service --label web --image web:latest
aws lightsail push-container-image --service-name demo-service --label service --image service:latest
```

## Deploy to AWS Lightsail

```shell
aws lightsail  create-container-service-deployment \
    --service-name demo-service \
    --containers file://containers.json \
    --public-endpoint file://endpoints.json
```


## References
- https://stedolan.github.io/jq/
- https://medium.com/geekculture/deploying-php-app-as-a-container-services-in-amazon-lightsail-with-github-actions-edbe68fcb45d