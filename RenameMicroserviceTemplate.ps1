param (
    [string]$Path,
    [string]$NewValue,
    [string]$OldValuePath,  
    [string]$NewValuePath  
)

$OldValue = "Area.Template"
$OldValuePath = "/area"

function Replace-StringInFiles {
    param (
        [string]$FilePath,
        [string]$OldValue,
        [string]$NewValue,
        [string]$OldValuePath,  
        [string]$NewValuePath  
    )
    
    if ($FilePath -like "*README.md") {
        Write-Host "Skipping replacement in file: $FilePath"
        return
    }

    Write-Host "Replacing in file: $FilePath"
    $content = Get-Content -Path $FilePath

    if ($FilePath -like "*.yml") {
        $OldValue = $OldValue.ToLower()
        $NewValue = $NewValue.ToLower()
        $OldValuePath = $OldValuePath.ToLower()
        $NewValuePath = $NewValuePath.ToLower()
    }

    $newContent = $content -replace $OldValue, $NewValue -replace $OldValuePath, $NewValuePath
    if ($content -ne $newContent) {
        Write-Host "Content replaced in file: $FilePath"
        $newContent | Set-Content -Path $FilePath
    } else {
        Write-Host "No replacement needed in file: $FilePath"
    }
}

function Rename-Items {
    param (
        [string]$Path,
        [string]$OldValue,
        [string]$NewValue,
        [string]$OldValuePath,  
        [string]$NewValuePath  
    )

    Get-ChildItem -Path $Path | Where-Object { $_.Name -notin "bin", "obj", ".idea" } | ForEach-Object {
        if ($_.PSIsContainer) {
            Write-Host "Entering directory: $($_.FullName)"
            Rename-Items -Path $_.FullName -OldValue $OldValue -NewValue $NewValue -OldValuePath $OldValuePath -NewValuePath $NewValuePath

            $NewName = $_.Name -replace $OldValue, $NewValue -replace $OldValuePath, $NewValuePath
            if ($_.Name -ne $NewName) {
                $NewFullName = Join-Path -Path $_.Parent.FullName -ChildPath $NewName
                Write-Host "Renaming directory: $($_.FullName) to $NewFullName"
                Rename-Item -Path $_.FullName -NewName $NewFullName
            } else {
                Write-Host "No renaming needed for directory: $($_.FullName)"
            }
        } else {
            if ($_.Name -eq "README.md") {
                Write-Host "Skipping file: $($_.FullName)"
                return
            }

            Replace-StringInFiles -FilePath $_.FullName -OldValue $OldValue -NewValue $NewValue -OldValuePath $OldValuePath -NewValuePath $NewValuePath

            $NewName = $_.Name -replace $OldValue, $NewValue -replace $OldValuePath, $NewValuePath
            if ($_.Name -ne $NewName) {
                $NewFullName = Join-Path -Path $_.DirectoryName -ChildPath $NewName
                Write-Host "Renaming file: $($_.FullName) to $NewFullName"
                Rename-Item -Path $_.FullName -NewName $NewFullName
            } else {
                Write-Host "No renaming needed for file: $($_.FullName)"
            }
        }
    }
}

$NewRootPath = $Path -replace $OldValue, $NewValue -replace $OldValuePath, $NewValuePath
if ($Path -ne $NewRootPath) {
    Write-Host "Renaming root directory: $Path to $NewRootPath"
    Rename-Item -Path $Path -NewName $NewRootPath
    $Path = $NewRootPath
} else {
    Write-Host "No renaming needed for root directory: $Path"
}

Rename-Items -Path $Path -OldValue $OldValue -NewValue $NewValue -OldValuePath $OldValuePath -NewValuePath $NewValuePath
