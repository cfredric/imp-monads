public class Test {
    public static void main(String args[]){
        runTests();
    }

    public static void runTests(){
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
