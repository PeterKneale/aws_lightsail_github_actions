# AWS Lightsail + Github Actions CI CD

## Setup AWS

 Create an IAM User with these permissions
```json
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Effect": "Allow",
      "Action": [
        "lightsail:CreateContainerServiceRegistryLogin",
        "lightsail:GetContainerImages"
      ],
      "Resource": "*"
    },
    {
      "Effect": "Allow",
      "Action": [
        "lightsail:RegisterContainerImage",
        "lightsail:CreateContainerServiceDeployment"
      ],
      "Resource": "arn:aws:lightsail:*:527559383819:ContainerService/*"
    }
  ]
}
```
## Setup AWS Lightsail

 Create a container service and database
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
    --master-username demo_user \
    --master-user-password demo_password
```

## Setup GitHub Secrets
Create secrets for use by the github workflow
- AWS_ACCESS_KEY_ID
- AWS_SECRET_ACCESS_KEY
- DB_HOST
- DB_PASSWORD
- DB_USERNAME

# Manual Builds

## Build Docker Images

```shell
docker build -t web -f src/Web/Dockerfile .
docker build -t service -f src/Service/Dockerfile .
```

## Push Docker Images

```shell
aws lightsail push-container-image --service-name demo-service --label web --image web:latest
aws lightsail push-container-image --service-name demo-service --label service --image service:latest
```


# References
- https://aws.amazon.com/lightsail/
- https://github.com/PeterKneale/aws_lightsail_github_actions
- https://stedolan.github.io/jq/
- https://medium.com/geekculture/deploying-php-app-as-a-container-services-in-amazon-lightsail-with-github-actions-edbe68fcb45d