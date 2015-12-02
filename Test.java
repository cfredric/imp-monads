public class Test {
    public static void main(String args[]){
        runTests();
    }

    public static void runTests(){
        assert "Just 4".equals(
            new Maybe<Integer>(new Integer(3))
            .fmap(new Function<Integer, Integer>(){
                public Integer apply(Integer x){
                    return new Integer(x.intValue() + 1);
                }
            }).toString());

        assert "Just 28".equals(
            new Maybe<Integer>(new Integer(3))
            .fmap(new Function<Integer, Integer>(){
                public Integer apply(Integer x){
                    return new Integer(x.intValue() + 1);
                }
            }).fmap(new Function<Integer, Integer>(){
                public Integer apply(Integer x){
                    return new Integer(x.intValue() * 7);
                }
            }).toString());

        assert "Nothing".equals(
            new Maybe<Integer>(new Integer(3))
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
            }).toString());

        System.out.println("Assertions passed");
    }
}
