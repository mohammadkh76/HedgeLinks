module.directive('stepWizard', () => ({
    restrict: 'E',
    scope: {
        steps: '=',
        currentStep: '='
    },
    templateUrl: '/App/Directives/Step/step-wizard-tmpl.html',
    link(scope) {
        scope.stepBoxClass = (stepIndex) => {
            if (scope.currentStep) {
                if (Number(scope.currentStep) === (stepIndex + 1)) {
                    return 'active';
                } else if (scope.currentStep > (stepIndex + 1)) {
                    return 'passed';
                }
            }
            return '';
        };
        // scope.progressbarValue = scope.currentStep * (100 / scope.steps.length);
    }
}));