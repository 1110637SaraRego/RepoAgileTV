
When debugging/testing:

//VALID REQUEST
convert https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt ./output/minhaCdn1.txt

^Note: Adjust to an existing path where you want to save the file.

//ERROR REQUEST - Invalid URL
convert http://logstorage.com/minhaCdn1.txt ./output/minhaCdn1.txt

//ERROR REQUEST - Invalid Path
convert https://s3.amazonaws.com/uux-itaas-static/minha-cdn-logs/input-01.txt ./output1/minhaCdn1.txt

^Note: Adjust to an invalid path.