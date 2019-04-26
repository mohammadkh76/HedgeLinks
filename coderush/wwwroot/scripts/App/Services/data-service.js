angModule.service('dataService',
    ['$http','Upload',
    function ($http,Upload) {
        this.get = function (url) {

            return $http.get(url);
        };
        this.post = function (url, data) {
            return $http.post(url, data)

        }
        this.getCurrentUser = function () {
           return this.get('/api/Common/GetCurrentUser').then(function (res) {
                return res.data

            }).catch(function (err) {
                console.log(err);

            })

        }
        this.upload=function(params){
            const uploadData={
                url: params.url,
                data: params.data,
                method: 'post',
                encType: 'multipart/form-data'
            };
            const {
                successCb = () => {},
                failureCb = () => {},
                progressCb = () => {}
            } = params;
            
            Upload  
                .upload(uploadData)
                .then(successCb, failureCb, progressCb)
                .catch(function (err) {
                    console.error(err);
                });
        }
        
    }]);