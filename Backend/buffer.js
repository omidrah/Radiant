const fs = require('fs');


function hexofstring(str){
    return  Buffer.from(value).toString('hex') ;
}

function hexofJson(){
    let jsonObject = {
        name: 'John',
        age: 30,
        city: 'New York'
      };
      
      let hexJson = JSON.stringify(jsonObject, (key, value) => 
        typeof value === 'string' ? Buffer.from(value).toString('hex') : value
      );
      return hexJson;
}
function hexWriteTofile(){

    let jsonObject = {
    name: 'John',
    age: 30,
    city: 'New York'
    };

    let hexJson = JSON.stringify(jsonObject, (key, value) => 
    typeof value === 'string' ? Buffer.from(value).toString('hex') : value
    );

    fs.writeFile('output.txt', hexJson, (err) => {
        if (err) throw err;
            console.log('The file has been saved!');
    });
}




module.exports ={
    hexofJson,
    hexofstring,
    hexWriteTofile
}