
 function buffer_alloc(buffLength) {
   //. The expression below creates a buffer with a byte size of 6
    const buf = Buffer.alloc(buffLength);
    console.log(buf);
    // This will print <Buffer 00 00 00 00 00 00>
  }
   function buffer_write(str){
    const buf = Buffer.alloc(100); // Creating a new Buffer
    const len = buf.write(str); // Writing to the Buffer
    return len;
    //The Buffer.write() function returns the length of the string, which is stored in the buffer.
  }

   function buffer_compare(str1,str2){
    var buf1 = Buffer.from(str1);
    var buf2 = Buffer.from(str2);
    var a = Buffer.compare(buf1, buf2);
    //This method returns -1, 0, or 1, depending on the result of the comparison.
    //This will return 0
    console.log(a);
  }

   function buffer_contac(){
    var buffer1 = Buffer.from('x');
    var buffer2 = Buffer.from('y');
    var buffer3 = Buffer.from('z');
    var arr = [buffer1, buffer2, buffer3];

    /*This will print buffer, !concat [ <Buffer 78>, <Buffer 79>, <Buffer 7a> ]*/
    console.log(arr);

    //concatenate buffer with Buffer.concat method
    var buf = Buffer.concat(arr);
    //Just like string concatenation, you can join two or more buffer objects into one object
    //This will print <Buffer 78 79 7a> concat successful
    console.log(buf);
  }
   const description = "Util By Buffer";


   module.exports =  {
    buffer_alloc,    
    buffer_compare,
    buffer_contac,
    buffer_write,
    description
   }
