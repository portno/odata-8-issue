import axios from 'axios';

(async () => {
    const requests = [];
    for (let i = 0; i < 100; i++) {
        // change to await and it will crash fewer times
        requests.push({
            id: i,
            request: axios.get("http://localhost:64149/odata/Books?$expand=Author", {
                responseType: 'arraybuffer'
            }).then((res) => {
                requests.filter(r => r.id == i)[0].response = Buffer.from(res.data).toString();
            })
        });
    }
    try {
        await axios.all(requests.map(r => r.request));
    } catch {
        //
    }
    
    console.log(requests.filter(r => r.response && r.response.indexOf('}{') >= 0).map(r => ({ id: r.id, response: r.response })));
    console.log("successful requests: ", requests.filter(r => r.response).length);
})();