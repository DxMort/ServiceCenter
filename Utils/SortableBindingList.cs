using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

public class SortableBindingList<T> : BindingList<T>
{
    private bool _isSorted;
    private ListSortDirection _sortDirection;
    private PropertyDescriptor _sortProperty;

    public SortableBindingList() : base() { }

    public SortableBindingList(IEnumerable<T> collection) : base(collection.ToList()) { }

    protected override bool SupportsSortingCore => true;

    protected override bool IsSortedCore => _isSorted;

    protected override PropertyDescriptor SortPropertyCore => _sortProperty;

    protected override ListSortDirection SortDirectionCore => _sortDirection;

    protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
    {
        var items = Items as List<T>;
        if (items == null) return;

        var comparer = new PropertyComparer<T>(prop, direction);
        items.Sort(comparer);

        _sortDirection = direction;
        _sortProperty = prop;
        _isSorted = true;

        ResetBindings();
    }

    private class PropertyComparer<U> : IComparer<U>
    {
        private readonly PropertyDescriptor _property;
        private readonly ListSortDirection _direction;

        public PropertyComparer(PropertyDescriptor property, ListSortDirection direction)
        {
            _property = property;
            _direction = direction;
        }

        public int Compare(U x, U y)
        {
            var valueX = _property.GetValue(x);
            var valueY = _property.GetValue(y);

            return _direction == ListSortDirection.Ascending
                ? Comparer<object>.Default.Compare(valueX, valueY)
                : Comparer<object>.Default.Compare(valueY, valueX);
        }
    }
}
