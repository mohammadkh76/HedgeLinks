generalModule.service('dataService',
    ['$http',
    function ($http) {
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
    }]);