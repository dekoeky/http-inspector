
# PREPARATION
$outdir = "$PSScriptRoot/../http-inspector/bin/Release/startup-timing-test"
$exe = "$outdir/http-inspector.exe"
# Remove-Item -Path $outdir -Recurse -Force -ErrorAction SilentlyContinue
# dotnet publish .\http-inspector\ -c Release -r win-x64 /p:PublishAot=true -o ./http-inspector/bin/Release/startup-timing-test


# TEST
$start = [Environment]::TickCount

# Start de app
$process = Start-Process -FilePath $exe -ArgumentList "--urls=http://localhost:5000" -PassThru

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

# Optioneel: afsluiten
Stop-Process -Id $process.Id
