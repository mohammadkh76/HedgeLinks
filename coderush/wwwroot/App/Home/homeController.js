module.controller("homeController", ["$scope", "dataService", function ($scope, dataService) {
    $scope.message = null;
    $scope.NewsLetterBtn=false;
    $scope.sendNewsLetter = function () {
        debugger;
            dataService.post('/api/HomeApi/sendNewsLetter',news).then(function (res) {
                debugger;
                toastr.success(res.data);

            }).catch(function (err) {
                debugger;
                toastr.error(res.error);
            })
        }
}]);
