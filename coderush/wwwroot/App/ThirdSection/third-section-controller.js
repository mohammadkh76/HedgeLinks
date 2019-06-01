adminModule.controller("thirdSectionController", ["$scope", "dataService", "$window", "$uibModal", "toaster", "convertorService", function ($scope, dataService, $window, $uibModal, toaster, convertorService) {
    $scope.tableLoading = true;
    
    $scope.getAll = function ({ successFunc, messages }) {
        dataService.get('/api/ThirdSection/GetAll/').then(function (res) {
            console.log(res.data);
            if (res.data.Data.length > 0) {
                debugger;
                $scope.data = res.data.Data;
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
   
    $scope.openDeleteModal = function (id) {
        $scope.selectedId = id;
        $scope.url = '/api/ThirdSection/Delete/' + id;
        $scope.href = '/ThirdSections/index';
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
        $scope.url = '/api/ThirdSection/Insert/';
        $scope.href = '/ThirdSections/index';
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/App/ThirdSection/insert-third-section.html',
            controller: 'insertThirdSectionController',
            scope: $scope,
            size: 'lg',
            windowClass: 'insert-modal-modal',
            appendTo: $('body')

        })
    }
    $scope.openEditModal = function (id) {
        $scope.selectedId = id;
        $scope.url = '/api/ThirdSection/GetEditData/' + id;
        $scope.href = '/ThirdSections/index';
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/App/ThirdSection/edit-third-section.html',
            controller: 'editThirdSectionController',
            scope: $scope,
            size: 'lg',
            windowClass: 'delete-modal',
            appendTo: $('body')
        })
    }
    
    }]);