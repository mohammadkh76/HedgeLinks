angModule.controller('addSendEmailController', ['$scope', 'dataService', 'Upload','$window','toaster', function ($scope, dataService, Upload,$window,toaster) {
    $scope.getEmailList=function(page){
        dataService.post('/api/SendEmailApi/GetEmail/',page).then(function (res) {
            $scope.totalEmailItem=res.data.emailCount;
            $scope.email=res.data.email;
        })
    }
    $scope.page= {
        take: 10,
        page: 1,
        takeAll: false
    }
    $scope.getEmailList($scope.page);
    $scope.emailPageChanged = function() {
        $scope.page.Take=10;
        $scope.page.Page=$scope.currentEmailPage;
        $scope.getEmailList($scope.page);
    };


    $scope.getPropList=function(page){
        dataService.post('/api/PropertiesApi/GetProperties/',page).then(function (res) {
            $scope.totalPropItem=res.data.PropCount;
            $scope.prop=res.data.properties;

        })
    }
    $scope.page= {
        take:10 ,
        page: 1,
        takeAll: false
    }
    $scope.getPropList($scope.page);
    $scope.propPageChanged = function() {
        $scope.page.Take=10;
        $scope.page.Page=$scope.currentPropPage;
        $scope.getPropList($scope.page);
    };



    $scope.data={
    emailsId:[],
    propIds:[]
};
$scope.isEmailSent=false;


    $scope.selectedEmail=function (Id){
        console.log(Id)
    }
    $scope.selectedProp=function (Id) {
        
    }

    $scope.submit = function (data) {
        $scope.isEmailSent=true;
        $scope.selectedIds=[];
        $scope.selectedPropIds=[];

        angular.forEach(data.emailsId, function (item, key) {

            if (item.isSelected) {
                $scope.selectedIds.push(key)

            }


        })
        angular.forEach(data.propIds, function (item, key) {

            if (item.isSelected) {
                $scope.selectedPropIds.push(key)

            }


        })
       
        console.log($scope.selectedIds);
        $scope.dataToSend={
            Subject:data.Subject,
            Message:data.Message,
            SelectedItems:$scope.selectedIds,
            SelectedProp:$scope.selectedPropIds[0],
        };
        dataService.post('/api/SendEmailApi/AddSendEmail/',$scope.dataToSend).then(function (res) {
            console.log(res);
                $window.location.href="/SendEmails/Index/";
            toaster.pop("error","hello","error");

            // $window.location.href="/SendEmails/Index";
        }).catch(function (res) {
            $scope.errorMessage=res.data;
            $scope.isEmailSent=false;
        })
    }

}]);
