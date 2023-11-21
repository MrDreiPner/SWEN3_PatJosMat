# $uri = "http://localhost:8888/test/test.png"
$uri = "http://localhost:8081/test/LoremIpsumDolor.pdf"
# $uploadPath = "C:\Users\max\Desktop\Document3.docx"
$uploadPath = "C:\dev\BIF5-SWKOM\CS\filestorage\docs\LoremIpsumDolor.pdf"

# Read the file content as a byte array
$fileContent = [System.IO.File]::ReadAllBytes($uploadPath)

# Set the headers and parameters
$headers = @{
    "Content-Type" = "application/octet-stream"
}

# Make the POST request
$response = Invoke-RestMethod -Uri $uri -Method POST -Headers $headers -Body $fileContent

# Process the response if needed
# $response

