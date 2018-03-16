$(function () {
    var prodSpecQty = +$('#prodSpecQty').val();

    var tableBody = $('#tblProductSpecifications tbody');

    // Кнопка подтверждения добавления характеристики
    // в категорию в модальном окне
    $('#btnAddSpecification').click(function () {
        let select = $('#specificationId');

        if (select.length == 0) {
            var specId = 0;
            var specName = $('#specificationName').val();
        } else {
            specId = +select.val();
            specName = $('option:selected', select).text();
        } // if

        let inputs = $('input[name$="ProductSpecification.Name"]', tableBody);

        // Проверяем, была ли добавлена характеристика с таким названием раньше
        if ($('input[name$="ProductSpecification.Name"]', tableBody).is(`[value="${specName}"]`)) {
            alert(`Характеристка со значением "${specName}" уже добавлена!`);
            return;
        } // if

        prodSpecQty = addProdSpecToTable(tableBody, specId, specName, prodSpecQty);
    });

    $('#CategoryId').change(function () {
        $.getJSON(
            '/Admin/Products/AjaxGetProductSpecifications',
            { categoryId: +$(this).val() },
            function (data) {
                for (var i = 0; i < data.length; i++)
                    addProdSpecWithValueToTable(tableBody, data[i], i);
            } // function
        );
    });

    $('#modalAddSpecification input[name="addCatType"]').change(function () {
        var divAddCatInputControl = $('#addCatInputControl');

        var label = document.createElement('label');
        label.htmlFor = 'specificationName';
        label.innerText = 'Название';

        // Проверяем выбранную радио-кнопку
        switch (this.id) {
            case 'addCatFromExists':
                $.getJSON(
                    '/Admin/Categories/AjaxGetSpecList',
                    { id: +$('#Id').val() },
                    function (data) {
                        var select = document.createElement('select');
                        select.id = 'specificationId'
                        select.className = 'form-control';

                        var option = document.createElement('option');
                        option.value = 0;
                        option.text = '-- Выберите категорию --';

                        select.options.add(option);

                        for (let item of data) {
                            option = document.createElement('option');
                            option.value = item.id;
                            option.text = item.name;

                            select.options.add(option);
                        } // for of

                        divAddCatInputControl.empty()
                            .append(label)
                            .append('<br />')
                            .append(select);
                    } // function
                );
                break;

            case 'addNewCat':
                let input = document.createElement('input');
                input.id = 'specificationName';
                input.type = 'text';
                input.className = 'form-control';

                divAddCatInputControl.empty()
                    .append(label)
                    .append('<br />')
                    .append(input);
                break;
        } // switch
    });

    $('#tblProductSpecifications tbody').sortable({
        stop: function (event, ui) {
            let offset = Math.round((ui.position.top - ui.originalPosition.top) / ui.item.height());

            let input = $('[id$=NumberInOrder]', ui.item);

            let itemOrigPosVal = +input.val();
            let itemNewPosVal = itemOrigPosVal + offset;

            let otherProdSpecs = $('[id$=NumberInOrder]', ui.item.parent());

            // Если элемент перетаскивался вниз
            if (offset > 0) {
                otherProdSpecs = otherProdSpecs
                    .filter(function () {
                        return this.value > itemOrigPosVal &&
                               this.value <= itemNewPosVal;
                    });

                otherProdSpecs.each(function () { this.value--; });
            }
            // Иначе, если элемент перетаскивался вверх
            else if (offset < 0) {
                otherProdSpecs = otherProdSpecs
                    .filter(function () {
                        return this.value < itemOrigPosVal &&
                               this.value >= itemNewPosVal
                    });

                otherProdSpecs.each(function () { this.value++; });
            } // if

            otherProdSpecs.parent().each(function (index) {
                let currInput = otherProdSpecs.eq(index);

                $(this).empty()
                    .append(currInput.val())
                    .append(currInput);
            });

            input.val(itemNewPosVal);

            input.parent().empty()
                .append(input.val())
                .append(input);
        }
    });
});