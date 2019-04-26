angModule.controller('forSaleDetailController', ['$scope', 'dataService', 'Upload', '$window', '$uibModal', function ($scope, dataService, Upload, $window, $uibModal) {
    const urlPath = $window.location.pathname;
    var x = urlPath.split('/');
    dataService.get('/api/TopImageApi/GetTopImage').then(function (res) {
        $scope.topimage = res.data.topimage[0];
    });
    dataService.get('/api/ForSaleApi/GetForSaleImage/' + x[3]).then(function (res) {
        $scope.forsaleimage = res.data.forsaleimage;
        $scope.rowCount = Array.apply(null, {length: Math.ceil($scope.forsaleimage.length / 4)}).map(Function.call, Number);

    });
    dataService.get('/api/ForSaleApi/GetForSaleDetail/' + x[3]).then(function (res) {
        $scope.detail = res.data.detail;
    });

}])