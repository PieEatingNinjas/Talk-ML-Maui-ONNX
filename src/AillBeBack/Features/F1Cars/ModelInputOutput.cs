using Microsoft.ML.Data;
using Microsoft.ML.Transforms.Image;

namespace AillBeBack.Features.F1Cars;

public class InputModel
{
    [ImageType(300, 300)]
    public MLImage? Image { get; set; } = null!;
}

public class OutputModel
{
    [ColumnName("model_output")]
    public float[]? ModelOutput { get; set; } = null!;
}
