adminModule.controller('deleteModalController', ['$scope', '$uibModalInstance', 'dataService', '$window', 'toaster', function ($scope, $uibModalInstance, dataService, $window, toaster) {
   console.log('hello modal!')
    
   $scope.confirmDel=function () {
         dataService.get($scope.url).then(function () {
             $scope.cancel();
        })
   }
    $scope.cancel = function () {
        $scope.isCanceled = true;
       $uibModalInstance.close();
       
    }
    $uibModalInstance.result.then(function () {
        if (!$scope.isCanceled) {
            $scope.tableLoading = true;
            $scope.getAll({
                successFunc() {
                    $scope.tableLoading = false;
                },
                messages() {
                    toaster.pop('success', 'Success', 'Your Record deleted successfully');

                }
            });


        }
    });
   
}]);