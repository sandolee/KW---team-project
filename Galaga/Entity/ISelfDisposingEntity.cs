namespace Galaga.Entity {
	/**
	 * EntityManager는 월드 밖으로 엔티티가 나가면 성능상의 이유로 Entity를 삭제합니다.
	 * 이런 동작을 원하지 않는 Entity 클래스는 ISelfDisposingEntity를 상속받고 엔티티를
	 * 삭제하는 매커니즘을 스스로 구현하여야 합니다.
	 */
	public interface ISelfDisposingEntity {
		
	}
}
