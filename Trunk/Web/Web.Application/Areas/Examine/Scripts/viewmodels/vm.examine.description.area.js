define('vm.examine.description.area',
    ['knockback'],
    function (kb) {

        function DescriptionArea(skeletonArea, components) {
            this.area = skeletonArea;
            this.areaComponents = kb.collectionObservable(components);
        }

        return DescriptionArea;
    });