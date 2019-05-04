adminModule.controller('deleteModalController', ['$scope', '$uibModalInstance', 'dataService','$window', function ($scope, $uibModalInstance, dataService,$window) {
   console.log('hello modal!')
    
   $scope.confirmDel=function () {
         dataService.get($scope.url).then(function () {
        $window.location.href=$scope.href;
        })
   }
   $scope.cancel=function () {
       $uibModalInstance.close();
       
   }
   
}]);