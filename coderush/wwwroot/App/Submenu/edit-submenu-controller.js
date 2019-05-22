adminModule.controller('EditSubmenuController', ['$scope', '$uibModalInstance', 'dataService', '$window', 'toaster', function ($scope, $uibModalInstance, dataService, $window, toaster) {
    console.log("in edit modal conroller")
    $scope.editDataLoading = true;
    $scope.page = {
        Current: $scope.currentPage,
        ItemInPage: 10
    }
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

    $scope.clearPath = function () {
        $scope.Path = "";

    }
    $scope.editDataLoading = true;

    dataService.get($scope.url).then(function (res) {
        if (res.data.Status == "success") {
           debugger
            $scope.Path = res.data.Data.Path;
            $scope.Name = res.data.Data.Name;
            $scope.selectedId = res.data.Data.Id;
            $scope.SelectedPage = parseInt(res.data.Data.MenuPathId);
            $scope.SelectedMenu = parseInt(res.data.Data.MenubarId);
            $scope.editDataLoading = false;

        }


    })
    $scope.confirmEdit = function () {
        $scope.editData = {
            Id:$scope.selectedId,
            Name: $scope.Name,
            SelectedPage: $scope.SelectedPage,
            SelectedMenu: $scope.SelectedMenu,
            Path: $scope.Path,
        }
        dataService.post('/api/Submenu/Edit/', $scope.editData).then(function (res) {
            if (res.data.Status == 'success') {
                $uibModalInstance.close();
            }

        })
        $uibModalInstance.result.then(function () {
            if (!$scope.isCanceled) {
                $scope.tableLoading = true;
                $scope.getAll({
                    successFunc() {
                        $scope.tableLoading = false;
                    },
                    messages() {
                        toaster.pop('success', 'Success', 'Your Record edited successfully');

                    }
                });

            }
           
        });
        $scope.cancel = function () {
            $scope.isCanceled = true;
            $uibModalInstance.close();

        }

    }
}])