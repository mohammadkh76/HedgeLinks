﻿adminModule.controller('InsertSubmenuController', ['$scope', '$uibModalInstance', 'dataService', '$window', 'toaster', function ($scope, $uibModalInstance, dataService, $window, toaster) {
    $scope.page = {
        Current: $scope.currentPage,
        ItemInPage: 10
    }
    $scope.isCanceled = false;
    $scope.menubarLoading = true;
    dataService.get('/api/Menupath/GetAllMenupath/').then(function (res) {
        if (res.data.Status == "Success") {
            
            $scope.pages = res.data.Data;
            

        }
        $scope.menubarLoading = false;

    })
    dataService.get('/api/Menubar/GetAllMenubar/').then(function (res) {
        if (res.data.Status == "Success") {

            $scope.menu = res.data.Data;


        }
        $scope.menubarLoading = false;

    })
   
    $scope.confirmInsert = function () {
        $scope.toSendData = {
            Name: $scope.Name,
            Path: $scope.Path,
            SelectedPage: $scope.SelectedPage,
            SelectedMenu: $scope.SelectedMenu,

        }
        dataService.post($scope.url, $scope.toSendData).then(function (res) {
            if (res.data.Status == "success") {

                //window.location.href = $scope.href;
                $uibModalInstance.close();



            }
        }).catch(function (err) {
            toaster.pop("error", "Error", err.data.Messages)

        }) 


        }   

    $uibModalInstance.result.then(function () {
        if (!$scope.isCanceled) {

            $scope.tableLoading = true;
            $scope.getAll({
                successFunc() {
                    $scope.tableLoading = false;
                },
                messages() {
                    toaster.pop('success', 'Success', 'Your Record inserted successfully');

                }
            });
        }

    });

    $scope.cancel = function () {
        $scope.isCanceled = true;
        $uibModalInstance.close();

    }

}]);