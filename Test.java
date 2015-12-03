public class Test {
    public static void main(String args[]){
        runTests();
    }

    public static void runTests(){
        Function<Integer, Maybe<Integer>> maybeAddThree = new Function<Integer, Maybe<Integer>>(){
            public Maybe<Integer> apply(Integer x){
                return new Just<Integer>(new Integer(x.intValue() + 3));
            }
        };

        Maybe<Integer> m = new Just<Integer>(new Integer(3))
            .fmap(new Function<Integer, Integer>(){
                public Integer apply(Integer x){
                    return new Integer(x.intValue() + 1);
                }
            });
        assert m.isJust;
        assert ((Just<Integer>) m).val.compareTo(new Integer(4)) == 0;

        Maybe<Integer> n = new Just<Integer>(new Integer(3))
            .fmap(new Function<Integer, Integer>(){
                public Integer apply(Integer x){
                    return new Integer(x.intValue() + 1);
                }
            }).fmap(new Function<Integer, Integer>(){
                public Integer apply(Integer x){
                    return new Integer(x.intValue() * 7);
                }
            });
        assert m.isJust;
        assert ((Just<Integer>) m).val.compareTo(new Integer(28)) == 0;

        Maybe<Integer> m1 = m.bind(maybeAddThree);
        assert m1.isJust;
        assert ((Just<Integer>) m1).val.compareTo(new Integer(25)) == 0;

        Maybe<Integer> o = new Just<Integer>(new Integer(3))
            .fmap(new Function<Integer, Integer>(){
                public Integer apply(Integer x){
                    return new Integer(x.intValue() + 1);
                }
            }).bind(new Function<Integer, Maybe<Integer>>(){
                public Maybe<Integer> apply(Integer x){
                    return new Nothing<Integer>();
                }
            }).fmap(new Function<Integer, Integer>(){
                public Integer apply(Integer x){
                    return new Integer(x.intValue() * 7);
                }
            });
        assert !o.isJust;
        Maybe<Integer> m2 = o.bind(maybeAddThree);
        assert !m2.isJust;

        assert "blah".equals(new Right<Integer, String>("blah").val);
        assert new Integer(5).compareTo(new Left<Integer, String>(new Integer(5)).val) == 0;
        assert new Integer(6).compareTo(((Right<String, Integer>)
                (new Right<String, Integer>(new Integer(2)))
                .fmap(new Function<Integer, Integer>(){
                    public Integer apply(Integer x){
                        return new Integer(x.intValue() * 3);
                    }
                })).val) == 0;

        System.out.println("Assertions passed");
    }
}
