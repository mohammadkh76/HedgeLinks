    adminModule.service('convertorService',
    [function () {
        this.toBase64 = function (file) {
            return new Promise((resolve) => {
                var reader = new FileReader();
                var base64 = "";
                reader.readAsDataURL(file);

                reader.onloadend = function ()
                {
             
                    base64 = reader.result;
                    return resolve({base64});
                }
            })
        }
        
        this.toBase64List=function(){
            
        }
    }]);
