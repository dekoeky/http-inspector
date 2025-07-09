
###################################################################################
# This script measures the time it takes for the first request, compared to the following requests
###################################################################################

# SETTINGS -------------------------------------------------------------------
$reporoot    = "$PSScriptRoot/../../../"
$project     = "$reporoot/http-inspector"
$baseOutdir  = "$reporoot/http-inspector/bin/Release/first-request-test"
$endpointUri = "http://localhost:5000/test"   # <- pas aan indien nodig
$requestCnt  = 10

# HELPERS --------------------------------------------------------------------
function Build-Version {
    param([string]$Name, [bool]$IsAot)

    Write-Host "=== Building $name version ==="
    $outdir = Join-Path $baseOutdir $Name
    Remove-Item $outdir -Recurse -Force -ErrorAction SilentlyContinue

    $aargs = @($project, "-c", "Release", "-r", "win-x64", "-o", $outdir)
    if ($IsAot) { $aargs += "/p:PublishAot=true" }
    else { $aargs += "/p:PublishAot=false" }

    dotnet publish @aargs
    return @{ Name = $Name; Exe = (Join-Path $outdir "http-inspector.exe") }
}

function Test-Version {
    param([string]$Name, [string]$Exe)

    $startdelay = 3
    $p  = Start-Process -FilePath $Exe -ArgumentList "--urls=http://localhost:5000" -PassThru

    "`n=== Waiting $startdelay seconds, to make sure app started... ==="
    Start-Sleep -Seconds $startdelay # vaste wachttijd
    

    $times = foreach ($i in 1..$requestCnt) {
        $sw = [System.Diagnostics.Stopwatch]::StartNew()
        try { Invoke-WebRequest $endpointUri -UseBasicParsing -TimeoutSec 2 | Out-Null } catch {}
        $sw.Stop()
        [PSCustomObject]@{ Version = $Name; Request = $i; TimeMs = $sw.ElapsedMilliseconds }
    }

    Stop-Process -Id $p.Id -Force
    return @{ ReqTimes = $times }
}

# BUILD (optional) -----------------------------------------------------------
# $aot    = Build-Version "AOT"    $true
# $nonAot = Build-Version "NonAOT" $false

# TEST -----------------------------------------------------------------------
$resAot    = Test-Version $aot.Name    $aot.Exe
$resNonAot = Test-Version $nonAot.Name $nonAot.Exe

# OUTPUT ---------------------------------------------------------------------
"`n=== Per-request timings (ms) ==="
$resAot.ReqTimes + $resNonAot.ReqTimes | Format-Table -GroupBy Version

# CALCULATE & COMPARE FIRST REQUEST VS AVG(2-N) ------------------------------
function Analyze-FirstRequest {
    param($Result, $VersionName)

    $times = $Result.ReqTimes
    $first = $times | Where-Object { $_.Request -eq 1 } | Select-Object -First 1
    $others = $times | Where-Object { $_.Request -gt 1 }

    $avg = [math]::Round(($others | Measure-Object -Property TimeMs -Average).Average, 2)
    $diffMs = $first.TimeMs - $avg
    $diffPct = [math]::Round(($diffMs / $avg) * 100, 1)

    Write-Host "`n[$VersionName] First request: $($first.TimeMs) ms"
    Write-Host "[$VersionName] Avg of request 2-${requestCnt}: $avg ms"
    Write-Host "[$VersionName] First request is $diffMs ms slower (+$diffPct%)"
}

Analyze-FirstRequest $resAot    "AOT"
Analyze-FirstRequest $resNonAot "NonAOT"


# Example Output :

# === Per-request timings (ms) ===


#    Version: AOT

# Version Request TimeMs
# ------- ------- ------
# AOT           1     17
# AOT           2      9
# AOT           3     10
# AOT           4      9
# AOT           5     10
# AOT           6     11
# AOT           7     10
# AOT           8     10
# AOT           9     10
# AOT          10     10


#    Version: NonAOT

# Version Request TimeMs
# ------- ------- ------
# NonAOT        1     78
# NonAOT        2     10
# NonAOT        3     10
# NonAOT        4     11
# NonAOT        5     10
# NonAOT        6     14
# NonAOT        7     11
# NonAOT        8     10
# NonAOT        9     10
# NonAOT       10     10


# [AOT] First request: 14 ms
# [AOT] Avg of request 2-10: 9.44 ms
# [AOT] First request is 4.56 ms slower (+48.3%)

# [NonAOT] First request: 76 ms
# [NonAOT] Avg of request 2-10: 11.44 ms
# [NonAOT] First request is 64.56 ms slower (+564.3%)