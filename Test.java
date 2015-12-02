public class Test {
    public static void main(String args[]){
        runTests();
    }

    public static void runTests(){
        Function<Integer, Maybe<?>> maybeAddThree = new Function<Integer, Maybe<?>>(){
            public Maybe<Integer> apply(Integer x){
                return Maybe.just(new Integer(x.intValue() + 3));
            }
        };

        Maybe<Integer> m = new Maybe<Integer>(new Integer(3))
            .fmap(new Function<Integer, Integer>(){
                public Integer apply(Integer x){
                    return new Integer(x.intValue() + 1);
                }
            });
        assert m.isJust;
        assert m.val.compareTo(new Integer(4)) == 0;

        Maybe<Integer> n = new Maybe<Integer>(new Integer(3))
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
        assert m.val.compareTo(new Integer(28)) == 0;

        Maybe<Integer> m1 = m.bind(maybeAddThree);
        assert m1.isJust;
        assert m1.val.compareTo(new Integer(25)) == 0;

        Maybe<Integer> o = new Maybe<Integer>(new Integer(3))
            .fmap(new Function<Integer, Integer>(){
                public Integer apply(Integer x){
                    return new Integer(x.intValue() + 1);
                }
            }).fmap(new Function<Integer, Integer>(){
                public Integer apply(Integer x){
                    return null;
                }
            }).fmap(new Function<Integer, Integer>(){
                public Integer apply(Integer x){
                    return new Integer(x.intValue() * 7);
                }
            });
        assert !o.isJust;
        Maybe<Integer> m2 = o.bind(maybeAddThree);
        assert !m2.isJust;

        assert "blah".equals(Either.right("blah").right);
        assert new Integer(5).compareTo(Either.left(new Integer(5)).left) == 0;
        assert new Integer(6).compareTo(
                Either.right(new Integer(2))
                .fmap(new Function<Integer, Integer>(){
                    public Integer apply(Integer x){
                        return new Integer(x.intValue() * 3);
                    }
                }).right) == 0;

        System.out.println("Assertions passed");
    }
}
