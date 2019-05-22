adminModule.controller("userController", ["$scope", "dataService", "$window", "$uibModal", "toaster", "convertorService", function ($scope, dataService, $window, $uibModal, toaster, convertorService) {
    $scope.currentPage = 1;
    $scope.tableLoading = true;
    $scope.page = {
        Current: $scope.currentPage,
        ItemInPage: 10
    }
    $scope.getAll = function ({ successFunc, messages }) {
        dataService.post('/api/User/GetAll/', $scope.page).then(function (res) {
            console.log(res.data);
            if (res.data.Data.length > 0) {
                $scope.data = res.data.Data;
                $scope.totalItems = res.data.Count;
            }
            successFunc && successFunc();
            messages && messages();

        })

    }
    $scope.getAll({
        successFunc() {
            $scope.tableLoading = false;
        }
    });


    $scope.setPage = function (pageNo) {
        $scope.currentPage = pageNo;
    };


    $scope.pageChanged = function (current) {
        $scope.tableLoading = true;
        $scope.page.Current = $scope.currentPage;
        //dataService.post('/api/Menupath/GetAll/', $scope.page2).then(function (res) {
        //    $scope.tableLoading = false;
        //    console.log(res.data);
        //    if (res.data.Data.length > 0) {
        //        $scope.data = res.data.Data;
        //        $scope.totalItems = res.data.Count;
        //    }
        //})
        $scope.getAll({
            successFunc() {
                $scope.tableLoading = false;
            }
        });
    };
    $scope.maxSize = 5;
    $scope.openChangePasswordModal= function (id) {
        $scope.selectedId = id;
        $scope.url = '/api/User/ChangePassword/' ;
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/App/User/change-password.html',
            controller: 'ChangePasswordController',
            scope: $scope,
            size: 'lg',
            windowClass: 'delete-modal',
            appendTo: $('body')
        })
    }
    $scope.openDeleteModal = function (id) {
        $scope.selectedId = id;
        $scope.url = '/api/User/Delete/' + id;
        $scope.href = '/User/index';
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/App/DeleteModal/delete-modal.html',
            controller: 'deleteModalController',
            scope: $scope,
            size: 'lg',
            windowClass: 'delete-modal',
            appendTo: $('body')
        })
    }

    $scope.openInsertModal = function () {
        $scope.url = '/api/User/Insert/';
        $scope.href = '/Users/index';
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/App/User/insert-user-modal.html',
            controller: 'InsertUserController',
            scope: $scope,
            size: 'lg',
            windowClass: 'insert-modal-modal',
            appendTo: $('body')

        })
    }
    $scope.openEditModal = function (id) {
        $scope.selectedId = id;
        $scope.url = '/api/Users/GetEditData/' + id;
        $scope.href = '/Users/index';
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/App/Menupath/edit-menupath.html',
            controller: 'EditMenupathModalController',
            scope: $scope,
            size: 'lg',
            windowClass: 'delete-modal',
            appendTo: $('body')
        })
    }
    
    }]);