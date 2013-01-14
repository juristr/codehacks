describe('The Backend', function() {
    
    beforeEach(function(){
    });

    afterEach(function(){
    });

    it('should return a list of people when invoking /api/person', function(){
        var fetchedResult;

        $.getJSON('/api/person', function(result){
            fetchedResult = result;
        });

        waitsFor(function(){
            return typeof(fetchedResult) !== 'undefined';
        }, 1000);

        runs(function(){
            expect(fetchedResult.length).toBe(3);
        });
    });

    it('should return a single person when invoking /api/person/4', function(){
        var fetchedResult;

        $.getJSON('/api/person/4', function(result){
            fetchedResult = result;
        });

        waitsFor(function(){
            return typeof(fetchedResult) !== 'undefined';
        }, 1000);

        runs(function(){
            expect(fetchedResult.firstname).toMatch('Juri');
            expect(fetchedResult.id).toMatch(4);
        });
    });

    it('should return a single person with a new id when executing a POST /api/person', function(){
        var fetchedResult;

        $.ajax({
            url: '/api/person',
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            data: { firstname: 'Juri' },
            success: function(result){
                fetchedResult = result;
            },
            error: function(error){
                var exception = JSON.parse(error.responseText);
                throw exception.ExceptionMessage;
            }
        });

        waitsFor(function(){
            return typeof(fetchedResult) !== 'undefined';
        }, 1000);

        runs(function(){
            expect(fetchedResult.id).toMatch(1234);
            expect(fetchedResult.firstname).toMatch('Juri');
        });
    });

});