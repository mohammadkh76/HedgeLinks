adminModule.controller('insertJobController', ['$scope', '$uibModalInstance', 'dataService', '$window', 'toaster', 'Upload', 'convertorService', function ($scope, $uibModalInstance, dataService, $window, toaster, Upload, convertorService) {
    $scope.page = {
        Current: $scope.currentPage,
        ItemInPage: 10
    }
    $scope.isCanceled = false;
    $scope.menubarLoading = true;
    $scope.uploadPreview = '/images/upload-image.png';
    $scope.changeUpload = function (file) {
        $scope.uploadLoading = true;
        convertorService.toBase64(file).then(function (res) {
            $scope.uploadPreview = res.base64;
            $scope.uploadLoading = false;
            $scope.$apply();
        }).catch(function (err) {
            console.log(err);
        });
    }
    dataService.get('/api/Menupath/GetAllMenupath/').then(function (res) {
        if (res.data.Status == "Success") {

            $scope.page = res.data.Data;


        }
        $scope.menubarLoading = false;

    })
    dataService.get('/api/JobType/GetAllJobType/').then(function (res) {
        if (res.data.Status == "Success") {

            $scope.jobType = res.data.Data;

        }
        $scope.menubarLoading = false;

    })
    dataService.get('/api/JobIndustry/GetAllJobIndustry/').then(function (res) {
        if (res.data.Status == "Success") {

            $scope.jobIndustry = res.data.Data;

        }
        $scope.menubarLoading = false;

    })
    $scope.clearPath = function () {
        $scope.ExternalLink = "";

    }


    $scope.confirmInsert = function () {
        debugger
        Upload.upload({

            url: '/api/Job/Insert',
            data: {
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
                ExternalLink: $scope.ExternalLink,
                File: $scope.File
            },
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