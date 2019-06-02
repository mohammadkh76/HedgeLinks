﻿adminModule.controller("jobIndustryController", ["$scope", "dataService", "$window", "$uibModal", "toaster", function ($scope, dataService, $window, $uibModal, toaster) {
    $scope.currentPage = 1;
    $scope.tableLoading = true;
    $scope.page = {
        Current: $scope.currentPage,
        ItemInPage: 10
    }
    $scope.getAll = function ({successFunc,messages}) {
        dataService.post('/api/JobIndustry/GetAll/', $scope.page).then(function (res) {
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
   
    $scope.openDeleteModal = function (id) {
        $scope.selectedId = id;
        $scope.url = '/api/JobIndustry/Delete/' + id;
        $scope.href = '/JobIndustries/index';
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/App/deleteModal/delete-modal.html',
            controller: 'deleteModalController',
            scope: $scope,
            size: 'lg',
            windowClass: 'delete-modal',
            appendTo: $('body')
        })
    }

    $scope.openInsertModal = function () {
        $scope.url = '/api/JobIndustry/Insert/';
        $scope.href = '/JobIndustries/index';
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/App/JobIndustry/insert-job-industry.html',
            controller: 'insertJobIndustryController',
            scope: $scope,
            size: 'lg',
            windowClass: 'insert-modal',
            appendTo: $('body')

        }).then(function (res) {


        })
    }
    $scope.openEditModal = function (id) {
        $scope.selectedId = id;
        $scope.url = '/api/JobIndustry/GetEditData/' + id;
        $scope.href = '/JobIndustries/index';
        var modalInstance = $uibModal.open({
            animation: true,
            templateUrl: '/App/JobIndustry/edit-job-industry.html',
            controller: 'editJobIndustryController',
            scope: $scope,
            size: 'lg',
            windowClass: 'delete-modal',
            appendTo: $('body')
        })
    }


}]);