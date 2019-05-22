adminModule.controller('ChangePasswordController', ['$scope', '$uibModalInstance', 'dataService', '$window', 'toaster', function ($scope, $uibModalInstance, dataService, $window, toaster) {
    console.log("Change Pssword modal");
    $scope.isCanceled = false;
    dataService.get('/api/User/GetUser/' + $scope.selectedId).then(function (res) {
    
        $scope.FirstName = res.data.Data.FirstName;
        $scope.Email = res.data.Data.Email;
        $scope.LastName = res.data.Data.LastName;


    })
    $scope.changePassword = function () {
        $scope.changePasswordLoading = true;
        $scope.data = {
            SelectedId: $scope.selectedId,
            OldPassword: $scope.OldPassword,
            Password: $scope.Password,
            ConfirmPassword: $scope.ConfirmPassword,
        }
        dataService.post($scope.url, $scope.data).then(function (res) {
            if (res.data.Status == "Success") {
                $scope.tableLoading = true;
                $scope.getAll({
                    successFunc() {
                        $scope.tableLoading = false;
                    },
                    messages() {
                        toaster.pop('success', 'Success', 'Your Password Changed Successfully');
                    }


                })
                $scope.changePasswordLoading = false;

                $scope.cancel();

            }

        }).catch(function (err) {
                $scope.changePasswordLoading = false;

            toaster.pop('error', 'Error', err.data.Messages);

        })
       
    }
   
    $scope.cancel = function () {
        $scope.isCanceled = true;
        $uibModalInstance.close();
    }

}])