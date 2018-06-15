var http =  require('http');  
var url = require('url');
var queryString = require('querystring'); 
var city = require("./cityCode");  
var request = require('sync-request');

http.createServer(function(req,res) {
    var body = "";
    req.on('data',function(chunk) {
        body+=chunk;
    });
    req.on('end',function() {
        body = queryString.parse(body);
        res.writeHead(200,{'Content-Type': 'text/html; charset=utf8'});
        // get weather
        // check city code;
        var cityName = body.cityName;
        var citycode = city[cityName];
        if(checkNumber(citycode)==1){
            console.log("--------"+citycode+"--------");
            var weatherUrl = "http://www.weather.com.cn/data/cityinfo/"+citycode+'.html';
            var result = JSON.parse(request('GET',weatherUrl).getBody()).weatherinfo;
            var response = {};
            response.temp2 = result.temp2;
            response.weather = result.weather;
            console.log(result);
            res.write(JSON.stringify(response));
        }
        else {
            res.write("Error");
        }
        res.end();
    });
}).listen(3000);

console.log("Server Start;");

function checkNumber(str){
    var reg = new RegExp("^[0-9]+$");
    if(reg.test(str))return 1;
    return 0; 
}

