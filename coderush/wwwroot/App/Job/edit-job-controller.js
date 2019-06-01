﻿adminModule.controller('editJobController', ['$scope', '$uibModalInstance', 'dataService', '$window', 'toaster', function ($scope, $uibModalInstance, dataService, $window, toaster) {
    console.log("in edit modal conroller")
    $scope.editDataLoading = true;
    $scope.page = {
        Current: $scope.currentPage,
        ItemInPage: 10
    }
    dataService.get('/api/Menupath/GetAllMenupath/').then(function (res) {
        if (res.data.Status == "Success") {
            $scope.page = res.data.Data;
            $scope.$apply();
        }
        $scope.menubarLoading = false;

    })
    dataService.get('/api/JobType/GetAllJobType/').then(function (res) {
        if (res.data.Status == "Success") {
            $scope.jobType = res.data.Data;
            $scope.$apply();
        }
    })
    dataService.get('/api/JobIndustry/GetAllJobIndustry/').then(function (res) {
        if (res.data.Status == "Success") {
            $scope.jobIndustry = res.data.Data;
            $scope.$apply();
        }
    })
    $scope.clearPath = function () {
        $scope.ExternalLink = "";

    }
    $scope.editDataLoading = true;

    dataService.get($scope.url).then(function (res) {
        if (res.data.Status == "success") {
            $scope.Title = res.data.Data.Title;
            $scope.Description = res.data.Data.Description;
            $scope.ShortDescription = res.data.Data.ShortDescription;
            $scope.RequiredExp = res.data.Data.RequiredExp;
            $scope.Address = res.data.Data.Address;
            $scope.Country = res.data.Data.Country;
            $scope.State - res.data.Data.State;
            $scope.City = res.data.Data.City;
            $scope.Compensation = res.data.Data.Compensation;
            $scope.DatePlaced = res.data.Data.DatePlaced;
            $scope.RequiredRole = res.data.Data.RequiredRole;
            $scope.CompanyName = res.data.Data.CompanyName;
            $scope.IsEasyApply = res.data.Data.isEasyApply;
            $scope.IsTrend = res.data.Data.isTrend;
            $scope.Keyword = res.data.Data.Keyword;
            $scope.SelectedPage = res.data.Data.MenuPathId.toString();
            $scope.SelectedJobIndustry = res.data.Data.JobIndustryId.toString();
            $scope.SelectedJobType = res.data.Data.JobTypeId.toString();
            $scope.OldImage = res.data.Data.FilePath;
            $scope.ExternalLink = res.data.Data.ExternalLink;
            $scope.uploadPreview = res.data.Data.FilePath;
                                                                               
            $scope.editDataLoading = false;

        }


    })
    $scope.confirmEdit = function () {
        
        $scope.editData = {
            Title: $scope.Title,
            Description: $scope.Description,
            ShortDescription: $scope.ShortDescription,
            RequiredExp: $scope.RequiredExp,
            Address: $scope.Address,
            Country: $scope.Country,
            State: $scope.State,
            City: $scope.City,
            Compensation: $scope.Compensation,
            DatePlaced: $scope.DatePlaced,
            RequiredRole: $scope.RequiredRole,
            CompanyName: $scope.CompanyName,
            IsEasyApply: $scope.IsEasyApply,
            IsTrend: $scope.IsTrend,
            Keyword: $scope.Keyword,
            MenuPathId: $scope.SelectedPage,
            JobIndustryId: $scope.SelectedJobIndustry,
            JobTypeId: $scope.SelectedJobType,
            OldImage: $scope.OldImage,
            $scope.ExternalLink = res.data.Data.ExternalLink,

            File: $scope.File
        }
        debugger;
        Upload.upload({
            url: '/api/Job/Edit',
            data: {
                Id: $scope.selectedId,
                Title: $scope.Title,
                Description: $scope.Description,
                ShortDescription: $scope.ShortDescription,
                RequiredExp: $scope.RequiredExp,
                Address: $scope.Address,
                Country: $scope.Country,
                State: $scope.State,
                City: $scope.City,
                Compensation: $scope.Compensation,
                DatePlaced: $scope.DatePlaced,
                RequiredRole: $scope.RequiredRole,
                CompanyName: $scope.CompanyName,
                IsEasyApply: $scope.IsEasyApply,
                IsTrend: $scope.IsTrend,
                Keyword: $scope.Keyword,
                MenuPathId: $scope.SelectedPage,
                JobIndustryId: $scope.SelectedJobIndustry,
                JobTypeId: $scope.SelectedJobType,
                OldImage: $scope.OldImage,
                $scope.ExternalLink : res.data.Data.ExternalLink,

                File: $scope.File
            }
        }).then(function (resp) {
            if (resp.data.Status == "Success") {
                $scope.userInsertLoading = false;

                $uibModalInstance.close();

            }
        }, function (err) {
            toaster.pop("error", "Error", err.data.Messages);
            $scope.userInsertLoading = false;

            console.log('Error status: ' + err);
        }, function (evt) {

            var progressPercentage = parseInt(100.0 * evt.loaded / evt.total);
            $scope.progress = progressPercentage;
            $scope.userInsertLoading = true;

        });
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