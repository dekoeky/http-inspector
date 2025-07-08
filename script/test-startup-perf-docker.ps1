
# PREPARE
# docker build -f ./http-inspector/Dockerfile.aot -t dekoeky/http-inspector:dev-aot .
# docker build -f ./http-inspector/Dockerfile.aot -t dekoeky/http-inspector:dev-non-aot .

$IMAGE = 'dekoeky/http-inspector:dev-aot'

# TEST
$start = [Environment]::TickCount

# Start de container (in achtergrond, detached)
Start-Process -NoNewWindow -FilePath "docker" -ArgumentList "run --rm --name startup-performance-test -p 5000:8080 $IMAGE"

# Wachten op succesvolle respons
do {
    Start-Sleep -Milliseconds 10
    try {
        $response = Invoke-WebRequest http://localhost:5000/health -UseBasicParsing -TimeoutSec 0.2
    } catch {
        $response = $null
    }
} while ($null -eq $response -or $response.StatusCode -ne 200)

$end = [Environment]::TickCount
$duration = $end - $start
Write-Host "Startup time: $duration ms"


# CLEANUP
docker container rm -f startup-performance-test