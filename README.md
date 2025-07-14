# http-inspector

🔎 HTTP Request Inspector Tool

## 🐋 Docker

[![Docker Image Badge](https://img.shields.io/badge/dekoeky-http--inspector-blue?style=for-the-badge&logo=docker)](https://hub.docker.com/r/dekoeky/http-inspector)
[![Docker Image Version](https://img.shields.io/docker/v/dekoeky/http-inspector?sort=semver&style=for-the-badge&logo=docker&label=version)](https://hub.docker.com/r/dekoeky/http-inspector/tags)
[![Docker Image Size](https://img.shields.io/docker/image-size/dekoeky/http-inspector?sort=semver&style=for-the-badge&logo=docker)](https://hub.docker.com/r/dekoeky/http-inspector/tags)

### Prepare for Building locally

```powershell
# Check if a multi-architecture buildx builder is present
docker buildx ls
# Example Output:
# NAME/NODE           DRIVER/ENDPOINT     STATUS     BUILDKIT   PLATFORMS
# multiarch*          docker-container
#  \_ multiarch0       \_ desktop-linux   inactive
# default             docker
#  \_ default          \_ default         running    v0.23.1    linux/amd64 (+3), linux/arm64, linux/arm (+2), linux/ppc64le, (2 more)
# desktop-linux       docker
#  \_ desktop-linux    \_ desktop-linux   running    v0.23.1    linux/amd64 (+3), linux/arm64, linux/arm (+2), linux/ppc64le, (2 more)

# If not, create a multi-architecture buildx builder instance
docker buildx create --name multiarch --use
```

### Build Locally & Push

```powershell
# Build & Push
docker buildx build `
    --platform linux/amd64,linux/arm64 `
    --tag dekoeky/http-inspector:latest `
    --tag dekoeky/http-inspector:0.0.1 `
    --push `
    -f .\http-inspector\Dockerfile.non-aot `
    .       
```

### Running Locally

```powershell
# Run the container
docker run --rm -p 5000:8080 dekoeky/http-inspector:latest
```

### Example Urls

- http://localhost:5080/
- http://localhost:5080/LandingPage?UserName=John
- http://localhost:5080/some/path?hello=world&TimeOfStartup=2025-07-04T12%3A13%3A14.8689932%2B02%3A00
- http://localhost:5080/health
- http://localhost:5080/health/live
- http://localhost:5080/health/ready
- http://localhost:5080/health/explain
- http://localhost:5080/about
- http://localhost:5080/endpoints
