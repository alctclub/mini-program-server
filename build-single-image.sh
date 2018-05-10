#!/bin/bash

cloudPrefix=ccr.ccs.tencentyun.com
localPrefix=docker.quncrm.com
if [ "$buildToCloud" = true ]; then 
  docker login --username=100001370990 ccr.ccs.tencentyun.com --password=abcd1234
  prefix=$cloudPrefix
else
  # docker login --username=admin docker.quncrm.com --password=abc123_
  prefix=$localPrefix  
fi

docker run --rm -i -v $(pwd):/app docker.quncrm.com/base/dotnet:2.0.0-sdk-jessie sh -c "cd /app/ALCT.Wechat.Mini.Program && dotnet build --force && cd /app/ALCT.Wechat.Mini.Program && dotnet publish"
docker build -t $prefix/alct/alct-wxmp:$releaseVersion ALCT.Wechat.Mini.Program
docker push $prefix/alct/alct-wxmp:$releaseVersion