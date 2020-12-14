let http = require("http");

let logs = [];

function sendRequests(iteration, totalNrOfRequests, nrOfCallsPerIteration = 50) {
    if (nrOfCallsPerIteration * iteration >= totalNrOfRequests) {
        console.log(logs)
        return;
    }

    for (let i = 0; i < nrOfCallsPerIteration; i++)
        http.get("http://localhost:44331/MedicalServices/List", res => {
            const { statusCode } = res;
            let time = (new Date()).getTime();

            res.setEncoding('utf8');
            let rawData = '';
            res.on('data', (chunk) => {
                console.log(chunk);
                rawData += chunk;
            });
            res.on('end', () => {
                try {
                    logs.push({
                        data: rawData,
                        statusCode: statusCode,
                        responseTime: (new Date()).getTime() - time,
                        time: new Date()
                    });
                } catch (e) {
                    console.error(e.message);
                }
            });
        }).on('error', (e) => {
            console.error(`Got error: ${e.message}`);
        });

    setTimeout(sendRequests, 3000, iteration + 1, totalNrOfRequests, nrOfCallsPerIteration);
}

sendRequests(0, 10, 1);